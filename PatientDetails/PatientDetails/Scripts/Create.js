$(document).ready(function () {
    $('#save').click(function (e) {
        e.preventDefault(); // Prevent default form submission

        // Retrieve field values and trim spaces
        const dosage = $('#dsgamount').val().trim();
        const drug = $('#drgname').val().trim();
        const patient = $('#pname').val().trim();

        // Input validation: Check if any field is empty
        if (!dosage || !drug || !patient) {
            $('#eAlert')
                .removeClass('d-none')
                .text('All fields are required.')
                .fadeIn();
            return; // Do not proceed to show the modal
        }

        // Set modal title and body dynamically
        $('#actionModalLabel').text('Create Record');
        $('#modalBody').html('<p>Are you sure you want to create this record?</p>');
        $('#createModal').modal('show');

        // Confirm button logic
        $('#confirmAction').off('click').click(function () {
            // AJAX request
            $.ajax({
                url: '/Patient/Create',
                method: 'POST',
                data: {
                    Dosage: dosage,
                    Drug: drug,
                    Patient: patient
                },
                success: function () {
                    // Hide error alert if it's visible
                    $('#eAlert').addClass('d-none');

                    // Show success alert
                    $('#sAlert')
                        .removeClass('d-none')
                        .text('Patient record has been created successfully.')
                        .fadeIn();

                    // Hide the modal and redirect
                    $('#createModal').modal('hide');
                    $('#createModal').on('hidden.bs.modal', function () {
                        window.location.href = '/Patient/Index';
                    });
                },
                error: function (xhr) {
                    const errorMessage = xhr.responseJSON?.message || 'Failed to create patient record.';
                    $('#eAlert').removeClass('d-none').text(errorMessage).fadeIn();
                    $('#createModal').modal('hide');
                }
            });
        });
    });

    // Clear form and hide alerts on reset
    $('#clr').on('click', function () {
        $('#create')[0].reset();
        $('#eAlert, #sAlert').addClass('d-none');
    });
});
