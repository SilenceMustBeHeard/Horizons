/* destination details */

(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        initDestinationMap();
    });


   
    // MAP INITIALIZATION
  

    function initDestinationMap() {
        const mapEl = document.getElementById('destinationMap');

        if (!mapEl) return;

        // Parse coordinates from data attributes
        const lat = parseFloat(mapEl.dataset.latitude);
        const lng = parseFloat(mapEl.dataset.longitude);

        // Skip if invalid
        if (isNaN(lat) || isNaN(lng)) {
            console.warn('Destination map: invalid coordinates');
            return;
        }

        // Extract metadata
        const name = mapEl.dataset.name || 'Destination';
        const country = mapEl.dataset.country || '';
        const continent = mapEl.dataset.continent || '';

        // Initialize Leaflet map
        const map = L.map(mapEl, {
            center: [lat, lng],
            zoom: 10,
            minZoom: 3,
            maxZoom: 18,
            zoomControl: true,
            scrollWheelZoom: false,   // prevent accidental zoom on scroll
            fadeAnimation: true,
            zoomAnimation: true,
            markerZoomAnimation: true
        });

        // Tile layer — OpenStreetMap
    L.tileLayer(
    'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    {
        attribution:
            '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 19
    }
).addTo(map);

        // Custom marker (matches Horizons marker style)
        const customIcon = L.divIcon({
            className: 'destination-marker',
            html:
                '<div class="horizons-marker">' +
                '<i class="fas fa-map-pin"></i>' +
                '</div>',
            iconSize: [34, 34],
            popupAnchor: [0, -17]
        });

        // Add marker
        const marker = L.marker([lat, lng], { icon: customIcon }).addTo(map);

        // Bind popup with destination name
        const popupContent =
            '<div class="destination-popup">' +
            '<strong>' + escapeHtml(name) + '</strong>' +
            (country || continent
                ? '<span class="destination-popup-location">' +
                  '<i class="fas fa-map-marker-alt"></i> ' +
                  escapeHtml(country) +
                  (country && continent ? ' • ' : '') +
                  escapeHtml(continent) +
                  '</span>'
                : '') +
            '</div>';

        marker.bindPopup(popupContent, {
            closeButton: true,
            autoPan: true,
            maxWidth: 240
        });

        // Scale control
        L.control.scale({
            position: 'bottomright',
            imperial: false
        }).addTo(map);

        // Fix sizing after layout settles
        setTimeout(function () {
            map.invalidateSize();
        }, 200);

        // Handle window resize
        window.addEventListener('resize', debounce(function () {
            map.invalidateSize();
        }, 150));
    }


    
    // helpers

    function escapeHtml(str) {
        if (!str) return '';

        return String(str).replace(/[&<>"']/g, function (m) {
            if (m === '&') return '&amp;';
            if (m === '<') return '&lt;';
            if (m === '>') return '&gt;';
            if (m === '"') return '&quot;';
            if (m === "'") return '&#39;';
            return m;
        });
    }

    function debounce(fn, delay) {
        let timer = null;

        return function () {
            clearTimeout(timer);
            timer = setTimeout(fn, delay);
        };
    }

})();