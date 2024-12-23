﻿
$(document).ready(function () {

    $('#save').click(function () {
        var dosage = $('#dsgamount').val();
        var drug = $('#drgname').val();
        var patient = $('#pname').val();
        // Input validation
        if (!dosage || !drug || !patient) {

            // Show error alert if validation fails
            $('#eAlert').removeClass('d-none').fadeIn();

            return; // Stop further execution if validation fails
        }

        $.ajax({
            url: '/Patient/Edit',
            method: 'POST',
            data: {
                Dosage: dosage,
                Drug: drug,
                Patient: patient
            },
            success: function (response) {
                // Show success alert on success
                $('#sAlert').removeClass('d-none').fadeIn();

                // Hide error alert if it is visible
                $('#eAlert').addClass('d-none').fadeOut();

                // Handle the success response
                console.log(response);

                // Redirect after a short delay if needed
                setTimeout(function () {
                    window.location.href = '/Patient/Index';
                }, 3000);
            },
            error: function (xhr, status, error) {
                // Handle errors
                console.error('Error: ' + error);
            }
        });
    });
    $('#clr').on('click', function () {
        $('#edit')[0].reset(); // Resets all input fields
    });
});
