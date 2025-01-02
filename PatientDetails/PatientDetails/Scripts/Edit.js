$(document).ready(function () {
    $('#save').click(function (e) {
        e.preventDefault(); // Prevent the default form submission behavior

        $('#actionModalLabel').text('Edit Record');
        $('#modalBody').html('<p>Are you sure you want to update this record?</p>');
        $('#editModal').modal('show');

        // Set up the confirm button inside the modal to submit the form
        $('#confirmAction').click(function () {
            const id = $('#id').val(); // Ensure the hidden ID field is included
            const dosage = $('#dsgamount').val(); // Allow empty values for partial updates
            const drug = $('#drgname').val().trim();
            const patient = $('#pname').val().trim();

            // Validate that ID is always present
            if (!id) {
                $('#eAlert').removeClass('d-none').text('Patient ID is required for updates.').fadeIn();
                $('#editModal').modal('hide'); // Hide modal if there's an error
                return;
            }

            // AJAX request to submit data
            $.ajax({
                url: '/Patient/Edit',
                method: 'POST',
                data: {
                    ID: id, // Include the ID in the request
                    Dosage: dosage || null, // Pass null for optional fields if empty
                    Drug: drug || null,
                    Patient: patient || null
                },
                success: function () {
                    $('#sAlert')
                        .removeClass('d-none')
                        .text('Patient record has been updated successfully.')
                        .fadeIn();

                    // Hide the modal immediately after showing the success alert
                    $('#editModal').modal('hide');

                    // Redirect immediately after the modal is hidden
                    window.location.href = '/Patient/Index';
                },
                error: function (xhr) {
                    const errorMessage = xhr.responseJSON?.message || 'Failed to update patient record.';
                    $('#eAlert').removeClass('d-none').text(errorMessage).fadeIn();
                    $('#editModal').modal('hide'); // Hide modal on error
                }
            });
        });
    });

    // Clear form fields on reset button click
    $('#clr').on('click', function () {
        $('#edit')[0].reset();
        $('#eAlert, #sAlert').addClass('d-none'); // Hide alerts when resetting
    });
});
