$(document).ready(function () {
    // Initialize all tooltips in the document
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Create the modal instance
    var authorModal = new bootstrap.Modal(document.getElementById('authorModal'));

    // Add a click event listener on the author name
    $('#author').click(function () {
        // Show the modal with Bootstrap's Modal API
        authorModal.show();
    });

    // Ensure the modal hides properly and removes the backdrop
    $('#authorModal').on('hidden.bs.modal', function () {
        // When the modal is hidden, ensure the backdrop is removed
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    });
});
