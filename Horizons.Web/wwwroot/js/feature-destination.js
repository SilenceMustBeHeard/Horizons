// Load featured destinations dynamically
async function loadFeaturedDestinations() {
    try {
        const response = await fetch('/api/destinations/map-data');
        const destinations = await response.json();

        // Take top 6 destinations by likes for featured section
        const topDestinations = destinations
            .sort((a, b) => (b.likes || 0) - (a.likes || 0))
            .slice(0, 6);

        const container = $('#featured-destinations');
        container.empty();

        if (topDestinations.length === 0) {
            container.html('<div class="col-12 text-center"><p class="text-muted">No destinations yet. Be the first to share your journey!</p></div>');
            return;
        }

        const row = $('<div class="destinations-grid"></div>');

        topDestinations.forEach(dest => {
            const card = $(`
                <div class="destination-card">
                    <div class="destination-image">
                        <img src="${dest.imageUrl || '/images/default-image.jpg'}" alt="${escapeHtml(dest.name)}" loading="lazy">
                        <span class="destination-badge">
                            <i class="fas fa-heart"></i> ${dest.likes || 0}
                        </span>
                    </div>
                    <div class="destination-content">
                        <div class="destination-location">
                            <i class="fas fa-map-marker-alt"></i> ${escapeHtml(dest.country || 'Unknown')}
                        </div>
                        <h3 class="destination-title">${escapeHtml(dest.name)}</h3>
                        <p class="destination-description">${escapeHtml((dest.description || '').substring(0, 120))}${(dest.description || '').length > 120 ? '...' : ''}</p>
                        <div class="destination-meta">
                            <span><i class="fas fa-calendar"></i> ${dest.createdAt ? new Date(dest.createdAt).toLocaleDateString() : 'Recently'}</span>
                            <a href="/Destination/Details/${dest.id}" class="btn btn-outline btn-sm">Read Story →</a>
                        </div>
                    </div>
                </div>
            `);
            row.append(card);
        });

        container.append(row);
    } catch (error) {
        console.error('Error loading featured destinations:', error);
        $('#featured-destinations').html('<div class="col-12 text-center"><p class="text-muted">Unable to load featured destinations.</p></div>');
    }
}

function escapeHtml(str) {
    if (!str) return '';
    return str.replace(/[&<>]/g, function (m) {
        if (m === '&') return '&amp;';
        if (m === '<') return '&lt;';
        if (m === '>') return '&gt;';
        return m;
    });
}

$(document).ready(function () {
    loadFeaturedDestinations();
});