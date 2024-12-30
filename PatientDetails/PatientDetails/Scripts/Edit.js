$(document).ready(function () {

    $(document).ready(function () {
        $('#save').click(function (e) {
            e.preventDefault(); // Prevent the default form submission behavior

            const id = $('#id').val(); // Ensure the hidden ID field is included
            const dosage = $('#dsgamount').val(); // Allow empty values for partial updates
            const drug = $('#drgname').val().trim();
            const patient = $('#pname').val().trim();

            // Validate that ID is always present
            if (!id) {
                $('#eAlert').removeClass('d-none').text('Patient ID is required for updates.').fadeIn();
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

                    // Redirect to index after success
                    setTimeout(() => window.location.href = '/Patient/Index', 2000);
                },
                error: function (xhr) {
                    const errorMessage = xhr.responseJSON?.message || 'Failed to update patient record.';
                    $('#eAlert').removeClass('d-none').text(errorMessage).fadeIn();
                }
            });
        });

        // Clear form fields on reset button click
        $('#clr').on('click', function () {
            $('#edit')[0].reset();
            $('#eAlert, #sAlert').addClass('d-none'); // Hide alerts when resetting
        });
    });

});
