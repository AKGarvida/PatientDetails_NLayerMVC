$(document).ready(function () {
    // Create the modal instance
    var authorModal = new bootstrap.Modal(document.getElementById('authorModal'));

    // Add a click event listener on the author name to show the modal
    $('#author').click(function () {
        authorModal.show();
    });

    // Add event listener for close button inside the modal to hide the modal
    $('#authorModal .btn-close').click(function () {
        authorModal.hide(); // Hide the modal when the close button is clicked
    });

    // Ensure the modal hides properly and removes the backdrop when the modal is hidden
    $('#authorModal').on('hidden.bs.modal', function () {
        // When the modal is hidden, ensure the backdrop is removed
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    });
});
