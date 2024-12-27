$(document).ready(function () {
    // ------------- Delete & Edit Modal Script -------------
    // Handle Delete Button Click dynamically for each row
    $(document).on('click', '.btn-danger', function () {
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

    // Handle Edit Button Click dynamically for each row
    $(document).on('click', '.btn-primary', function () {
        $('#actionModalLabel').text('Edit Record');
        $('#modalBody').html('<p>Do you want to edit this record?</p>');
        $('#confirmAction').text('Edit').removeClass('btn-danger').addClass('btn-primary');
        $('#actionModal').modal('show');
    });

    // ------------- Filtered Search Script -------------
    function ajaxFilterTable() {
        // Get filter values
        const filters = {
            date: $("#date").val(),
            dosage: $("#dosage").val(),
            drug: $("#drug").val(),
            patient: $("#patient").val(),
        };

        // Send AJAX request
        $.ajax({
            url: "/Patient/FilterPatients", // Adjust this URL to match your route
            type: "GET",
            data: filters,
            success: function (data) {
                // Update table body with new data
                const tbody = $("table tbody");
                tbody.empty(); // Clear existing rows

                if (data.length > 0) {
                    // Populate rows with returned data
                    data.forEach(patient => {
                        tbody.append(`
                            <tr>
                                <td>
                                    <button type="button" class="btn btn-primary me-2" data-bs-toggle="tooltip" title="Edit Record">Edit</button>
                                    <button type="button" class="btn btn-danger" data-bs-toggle="tooltip" title="Delete Record">Delete</button>
                                </td>
                                <td>${new Date(patient.ModifiedDate).toLocaleDateString()}</td>
                                <td>${patient.Dosage}</td>
                                <td>${patient.Drug}</td>
                                <td>${patient.Patient}</td>
                                <td></td>
                            </tr>
                        `);
                    });
                } else {
                    // Display a message if no records are found
                    tbody.append(`
                        <tr>
                            <td colspan="6" class="text-center">No records found</td>
                        </tr>
                    `);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error fetching data:", error);
            },
        });
    }

    // Attach event listeners to filter inputs
    $("#date, #dosage, #drug, #patient").on("keyup change", ajaxFilterTable);

    // Reset filters
    $("#resetBtn").on("click", function () {
        $("#date").val("");
        $("#dosage").val("");
        $("#drug").val("");
        $("#patient").val("");
        ajaxFilterTable();
    });

    // Initial table load
    ajaxFilterTable();
});
