// Load featured destinations dynamically
async function loadFeaturedDestinations() {
    try {
        const response = await fetch('/api/destinations/map-data');
        const destinations = await response.json();

        // Take top 4 destinations by likes
        const topDestinations = destinations
            .sort((a, b) => (b.likes || 0) - (a.likes || 0))
            .slice(0, 4);

        const container = $('#featured-destinations');
        container.empty();

        if (topDestinations.length === 0) {
            container.html('<div class="col-12 text-center"><p class="text-muted">No destinations yet. Be the first to add one!</p></div>');
            return;
        }

        const row = $('<div class="row g-4"></div>');

        topDestinations.forEach(dest => {
            const col = $(`
                    <div class="col-lg-3 col-md-6">
                        <div class="featured-card">
                            <img src="${dest.imageUrl || '/images/default-image.jpg'}" class="featured-card-img" alt="${dest.name}">
                            <div class="featured-card-body">
                                <h5 class="featured-card-title">${dest.name}</h5>
                                <p class="featured-card-terrain"><i class="fas fa-location-dot"></i> ${dest.country}</p>
                                <div class="mt-2">
                                    <i class="fas fa-heart" style="color: var(--sport-primary);"></i> ${dest.likes || 0} adventurers
                                </div>
                                <a href="/Destination/Details/${dest.id}" class="btn btn-sm btn-primary w-100 mt-3">
                                    <i class="fas fa-eye"></i> View Destination
                                </a>
                            </div>
                        </div>
                    </div>
                `);
            row.append(col);
        });

        container.append(row);
    } catch (error) {
        console.error('Error loading featured destinations:', error);
        $('#featured-destinations').html('<div class="col-12 text-center"><p class="text-muted">Unable to load featured destinations.</p></div>');
    }
}

$(document).ready(function () {
    loadFeaturedDestinations();
});