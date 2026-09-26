/* destination edit */

(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        initImagePreview();
    });


   
    // live image preview
   

    function initImagePreview() {
        const imageUrlInput = document.getElementById('ImageUrl');
        const imagePreview = document.getElementById('imagePreview');

        if (!imageUrlInput || !imagePreview) return;

        imageUrlInput.addEventListener('input', function () {
            const url = this.value.trim();

            if (url) {
                imagePreview.src = url;

                imagePreview.onerror = function () {
                    this.src = '/images/default-image.jpg';
                };
            }
        });
    }

})();