$(document).ready(function () {
    $('#save').click(function (e) {
        e.preventDefault();

        const id = $('#id').val().trim();
        const dosage = $('#dsgamount').val().trim();
        const drug = $('#drgname').val().trim();
        const patient = $('#pname').val().trim();

        // Input validation: Check if all fields are filled
        if (!id || !dosage || !drug || !patient) {
            $('#eAlert')
                .removeClass('d-none')
                .text('All input required.')
                .fadeIn();
            return; // Stop execution if validation fails
        }

        // Set modal title and body dynamically
        $('#actionModalLabel').text('Edit Record');
        $('#modalBody').html('<p>Are you sure you want to update this record?</p>');
        $('#editModal').modal('show');

        // Confirm button logic
        $('#confirmAction').off('click').click(function () {
            // AJAX request
            $.ajax({
                url: '/Patient/Edit',
                method: 'POST',
                data: {
                    ID: id, // Include the ID in the request
                    Dosage: dosage,
                    Drug: drug,
                    Patient: patient
                },
                success: function () {
                    $('#eAlert').addClass('d-none'); // Hide error alert on success
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
