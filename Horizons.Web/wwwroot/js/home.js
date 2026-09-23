// home page

(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        initHomeMap();
    });


    
    // HOME MAP
   

    async function initHomeMap() {
        const mapEl = document.getElementById('homeMap');

        if (!mapEl) return;

        // Initialize map — world view
        const map = L.map(mapEl, {
            center: [25, 10],
            zoom: 2,
            minZoom: 2,
            maxZoom: 12,
            zoomControl: false,
            scrollWheelZoom: false,
            dragging: true,
            attributionControl: true,
            fadeAnimation: true,
            zoomAnimation: true
        });

        // Tile layer
     L.tileLayer(
    'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    {
        attribution:
            '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 19
    }
).addTo(map);

        // Zoom control in bottom-right
        L.control.zoom({ position: 'bottomright' }).addTo(map);

        // Load destinations
        try {
            const response = await fetch('/api/destinations/map-data');

            if (!response.ok) {
                throw new Error('Failed to load destinations');
            }

            const destinations = await response.json();

            if (!Array.isArray(destinations) || destinations.length === 0) {
                return;
            }

            // Add markers for each destination
            destinations.forEach(function (dest) {
                if (!dest.latitude || !dest.longitude) return;

                const customIcon = L.divIcon({
                    className: 'home-marker',
                    html:
                        '<div class="home-marker-dot">' +
                        '<i class="fas fa-map-pin"></i>' +
                        '</div>',
                    iconSize: [26, 26],
                    popupAnchor: [0, -13]
                });

                const marker = L.marker(
                    [dest.latitude, dest.longitude],
                    { icon: customIcon }
                );

                marker.bindPopup(
                    '<div class="home-popup">' +
                    '<strong>' + escapeHtml(dest.name) + '</strong>' +
                    (dest.country
                        ? '<span class="home-popup-location">' +
                          '<i class="fas fa-map-marker-alt"></i> ' +
                          escapeHtml(dest.country) +
                          '</span>'
                        : '') +
                    '</div>',
                    {
                        closeButton: false,
                        autoPan: true,
                        maxWidth: 200
                    }
                );

                marker.addTo(map);
            });

        } catch (error) {
            console.warn('Home map: could not load destinations', error);
        }

        // Fix sizing
        setTimeout(function () {
            map.invalidateSize();
        }, 200);
    }


    
    // HELPERS
    

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

})();