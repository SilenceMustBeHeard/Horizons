$(document).ready(function () {
    function showToast(title, message, type = 'success') {
        let toastContainer = $('.toast-container');
        if (toastContainer.length === 0) {
            $('body').append('<div class="toast-container position-fixed bottom-0 end-0 p-3" style="z-index: 1100"></div>');
            toastContainer = $('.toast-container');
        }

        const icons = {
            success