$(document).ready(function () {
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
