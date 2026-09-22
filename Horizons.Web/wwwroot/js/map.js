// map.js - JavaScript for the  map page

(function () {
    'use strict';
// global variables

    let map = null;
    let markerLayer = null;
    let destinations = [];
    let isMapInitialized = false;
    let currentFilter = 'all';
    let isLoading = false;


// helpful utility functions

    function escapeHtml(str) {
        if (!str) return '';

        return String(str).replace(/[&<>]/g, function (m) {
            if (m === '&') return '&amp;';
            if (m === '<') return '&lt;';
            if (m === '>') return '&gt;';
            return m;
        });
    }

    function getMarkerColor(continent) {
        const colors = {
            'asia': '#b8934a',
            'europe': '#a86238',
            'north america': '#5a7d94',
            'south america': '#6b8248',
            'africa': '#b8934a',
            'oceania': '#c17a4a',
            'antarctica': '#7a9bb5'
        };

        return colors[(continent || '').toLowerCase()] || '#b8934a';
    }

    function animateNumber(element, start, end, duration) {
        if (!element) return;

        const range = end - start;
        const increment = range / (duration / 16);

        let current = start;

        const timer = setInterval(function () {
            current += increment;

            if (current >= end) {
                clearInterval(timer);
                element.textContent = Math.round(end).toLocaleString();
            } else {
                element.textContent = Math.round(current).toLocaleString();
            }
        }, 16);
    }

    function formatDate(dateString) {
        if (!dateString) return 'Unknown';

        try {
            return new Date(dateString).toLocaleDateString();
        } catch {
            return 'Unknown';
        }
    }

// global functions to be accessible from HTML



    window.goToDestination = function (id) {
        if (id) {
            window.location.href = '/Destination/Details/' + id;
        }
    };

    window.locateOnMap = function (lat, lng) {
        if (map && !isNaN(lat) && !isNaN(lng)) {
            map.setView([lat, lng], 12);
        }
    };


  // initialize map and load data when the DOM is ready

    document.addEventListener('DOMContentLoaded', function () {
        initMap();
        loadDestinations();
        setupFilters();
        setupSearch();
    });


  // map initialization

    function initMap() {
        if (map !== null) return;

        map = L.map('map', {
            minZoom: 2,
            maxZoom: 18,
            zoomControl: true,
            fadeAnimation: true,
            zoomAnimation: true,
            markerZoomAnimation: true
        }).setView([20, 0], 2);

      L.tileLayer(
    'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    {
        attribution:
            '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 19
    }
).addTo(map);

        L.control.scale().addTo(map);

        markerLayer = L.layerGroup().addTo(map);

        isMapInitialized = true;

        setTimeout(function () {
            if (map) map.invalidateSize();
        }, 300);
    }


    // data loading

    async function loadDestinations() {
        if (isLoading) return;

        isLoading = true;

        const listEl = document.getElementById('destinationsList');

        if (listEl) {
            listEl.innerHTML =
                '<div class="map-loading-content">' +
                '<div class="map-loading-spinner"></div>' +
                '<span>Loading destinations...</span>' +
                '</div>';
        }

        try {
            const response = await fetch('/api/destinations/map-data');

            if (!response.ok) {
                throw new Error('Network response was not ok: ' + response.status);
            }

            const data = await response.json();

            destinations = Array.isArray(data) ? data : [];

            if (destinations.length === 0) {
                if (listEl) {
                    listEl.innerHTML =
                        '<div style="color: var(--text-muted); text-align: center; padding: 2rem;">' +
                        '<i class="fas fa-map-pin" style="font-size: 2rem; display: block; margin-bottom: 1rem;"></i>' +
                        'No destinations found' +
                        '</div>';
                }
                return;
            }

            updateStats(destinations);
            renderMarkers(destinations);
            renderSidebar(destinations);

        } catch (error) {
            console.error('Error fetching destinations:', error);

            if (listEl) {
                listEl.innerHTML =
                    '<div style="color: var(--color-danger); text-align: center; padding: 2rem;">' +
                    '<i class="fas fa-exclamation-triangle" style="font-size: 2rem; display: block; margin-bottom: 1rem;"></i>' +
                    'Failed to load destinations' +
                    '</div>';
            }
        } finally {
            isLoading = false;
        }
    }

// stats
    function updateStats(destinationsToRender) {
        const uniqueCountries = [];
        const countrySet = {};

        let totalKm = 0;

        destinationsToRender.forEach(function (d) {
            if (d.country && !countrySet[d.country]) {
                countrySet[d.country] = true;
                uniqueCountries.push(d.country);
            }

            if (d.distance) {
                totalKm += d.distance;
            }
        });

        const destCount = document.getElementById('destinationCount');
        const countriesCount = document.getElementById('countriesCount');
        const totalDistance = document.getElementById('totalDistance');

        if (destCount) animateNumber(destCount, 0, destinationsToRender.length, 1000);
        if (countriesCount) animateNumber(countriesCount, 0, uniqueCountries.length, 1000);
        if (totalDistance) animateNumber(totalDistance, 0, totalKm, 1500);
    }


// marker rendering and popups

    function renderMarkers(destinationsToRender) {
        if (!markerLayer) return;

        markerLayer.clearLayers();

        const filtered = currentFilter === 'all'
            ? destinationsToRender
            : destinationsToRender.filter(function (d) {
                return (d.continent || '').toLowerCase() === currentFilter;
            });

        filtered.forEach(function (dest) {
            if (!dest.latitude || !dest.longitude) return;

            const markerColor = getMarkerColor(dest.continent);

            const customIcon = L.divIcon({
                className: 'custom-marker',
                html:
                    '<div class="horizons-marker" style="background: ' + markerColor + ';">' +
                    '<i class="fas fa-map-pin"></i>' +
                    '</div>',
                iconSize: [34, 34],
                popupAnchor: [0, -17]
            });

            const marker = L.marker([dest.latitude, dest.longitude], {
                icon: customIcon
            });

            marker.bindPopup(createPopupContent(dest), {
                maxWidth: 300,
                minWidth: 260,
                autoPan: true
            });

            marker.addTo(markerLayer);
        });
    }

    function createPopupContent(dest) {
        const description = dest.description
            ? dest.description.substring(0, 100)
            : 'No description available';

        const imageUrl = dest.imageUrl || '/images/default-image.jpg';

        const escapedName = escapeHtml(dest.name);
        const escapedCountry = escapeHtml(dest.country || 'Unknown');
        const escapedContinent = escapeHtml(dest.continent || 'Unknown');
        const escapedDescription = escapeHtml(description);

        return (
            '<div class="map-popup">' +
            '<img src="' + imageUrl + '" alt="' + escapedName + '" class="map-popup-image" onerror="this.src=\'/images/default-image.jpg\'">' +
            '<div class="map-popup-body">' +
            '<div class="map-popup-location"><i class="fas fa-map-marker-alt"></i> ' + escapedCountry + ' • ' + escapedContinent + '</div>' +
            '<h4 class="map-popup-title">' + escapedName + '</h4>' +
            '<p class="map-popup-description">' + escapedDescription + '...</p>' +
            '<a href="/Destination/Details/' + dest.id + '" class="map-popup-action">Explore Journey <i class="fas fa-arrow-right"></i></a>' +
            '</div>' +
            '</div>'
        );
    }


  // sidebar rendering and filtering

    function renderSidebar(destinationsToRender) {
        const container = document.getElementById('destinationsList');

        if (!container) return;

        if (destinationsToRender.length === 0) {
            container.innerHTML =
                '<div style="color: var(--text-muted); text-align: center; padding: 2rem;">' +
                'No destinations found' +
                '</div>';
            return;
        }

        let html = '';

        for (let i = 0; i < destinationsToRender.length; i++) {
            const dest = destinationsToRender[i];

            const escapedName = escapeHtml(dest.name);
            const escapedCountry = escapeHtml(dest.country || 'Unknown');

            const imageUrl = dest.imageUrl || '/images/default-image.jpg';

            const continent = (dest.continent || '').toLowerCase();

            const rank = dest.rank ? '#' + dest.rank : 'featured';
            const id = dest.id;

            html +=
                '<a href="/Destination/Details/' + id + '" ' +
                'class="map-sidebar-item" ' +
                'data-id="' + id + '" ' +
                'data-continent="' + continent + '">' +

                '<span class="map-sidebar-item-title">' + escapedName + '</span>' +

                '<span class="map-sidebar-item-meta">' +
                '<i class="fas fa-map-marker-alt"></i> ' + escapedCountry +
                ' • ' + rank +
                '</span>' +

                '</a>';
        }

        container.innerHTML = html;
    }


  // filter functionality for the sidebar

    function setupFilters() {
        const filterTags = document.querySelectorAll('.filter-tag');

        filterTags.forEach(function (tag) {
            tag.addEventListener('click', function () {
                const filter = tag.dataset.filter;

                if (currentFilter === filter) return;

                currentFilter = filter;

                filterTags.forEach(function (t) {
                    t.classList.remove('active');
                });

                tag.classList.add('active');

                const cards = document.querySelectorAll('.map-sidebar-item');

                cards.forEach(function (card) {
                    if (filter === 'all') {
                        card.style.display = '';
                    } else {
                        const cardContinent = card.dataset.continent;
                        card.style.display = cardContinent === filter ? '' : 'none';
                    }
                });

                renderMarkers(destinations);
            });
        });
    }


 // search functionality for the sidebar

    function setupSearch() {
        const searchInput = document.getElementById('searchDestinations');

        if (searchInput) {
            searchInput.addEventListener('input', function (e) {
                const searchTerm = e.target.value.toLowerCase();
                const cards = document.querySelectorAll('.map-sidebar-item');

                cards.forEach(function (card) {
                    const title = card
                        .querySelector('.map-sidebar-item-title')
                        ?.textContent?.toLowerCase() || '';

                    const location = card
                        .querySelector('.map-sidebar-item-meta')
                        ?.textContent?.toLowerCase() || '';

                    card.style.display =
                        (title.includes(searchTerm) || location.includes(searchTerm))
                            ? ''
                            : 'none';
                });
            });
        }
    }


    
    // reset map size on window resize to ensure proper rendering
  

    window.addEventListener('resize', function () {
        if (map) {
            setTimeout(function () {
                map.invalidateSize();
            }, 100);
        }
    });

})();