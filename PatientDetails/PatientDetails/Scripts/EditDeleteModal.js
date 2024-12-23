$(document).ready(function () {

    // Handle Delete Button Click
    $('.btn-danger').click(function () {
        // Show confirmation modal
        $('#actionModalLabel').text('Delete Record');
        $('#modalBody').html('<p>Are you sure you want to delete this record?</p>');
        $('#confirmAction').text('Delete').removeClass('btn-primary').addClass('btn-danger');
        $('#actionModal').modal('show');

        // Save the row reference to delete after confirmation
        const rowToDelete = $(this).closest('tr'); // Get the row containing the clicked button

        // Confirm Action
        $('#confirmAction').off('click').on('click', function () {
            rowToDelete.remove(); // Remove the row
            $('#actionModal').modal('hide'); // Hide the modal
        });
    });

    // Handle Edit Button Click
    $('.btn-primary').click(function () {
        $('#actionModalLabel').text('Edit Record');
        $('#modalBody').html('<p>Do you want to edit this record?</p>');
        $('#confirmAction').text('Edit').removeClass('btn-danger').addClass('btn-primary');
        $('#actionModal').modal('show');
    });
});
