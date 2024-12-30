$(document).ready(function () {
    // ------------- Delete & Edit Modal Script -------------
    $(document).on('click', '.btn-danger', function () {
        const rowToDelete = $(this).closest('tr');
        const patientId = rowToDelete.data('id'); 

        $('#actionModalLabel').text('Delete Record');
        $('#modalBody').html('<p>Are you sure you want to delete this record?</p>');
        $('#confirmAction').text('Delete').removeClass('btn-primary').addClass('btn-danger');
        $('#actionModal').modal('show');

        $('#confirmAction').off('click').on('click', function () {
            $.ajax({
                url: '/Patient/DeletePatient',
                type: 'POST',
                data: { ID: patientId },
                success: function (response) {
                    rowToDelete.remove();
                    $('#actionModal').modal('hide');
                },
                error: function (xhr, status, error) {
                    console.error("Error deleting record:", error);
                    $('#actionModal').modal('hide');
                },
            });
        });
    });

    // ------------- Edit Record Script -------------
    $(document).on('click', '.btn-primary', function () {
        const patientId = $(this).closest('tr').data('id');
        
        $('#actionModalLabel').text('Edit Record');
        $('#modalBody').html('<p>Do you want to edit this record?</p>');
        $('#confirmAction').text('Edit').removeClass('btn-danger').addClass('btn-primary');
        $('#actionModal').modal('show');

        // Confirm edit action
        $('#confirmAction').off('click').on('click', function () {
            // Redirect to the Edit page once confirmed
            window.location.href = `/Patient/Edit/${patientId}`;
        });
    });

    // ------------- Filtered Search Script -------------
    function ajaxFilterTable() {
        const filters = {
            date: $("#date").val(),
            dosage: $("#dosage").val(),
            drug: $("#drug").val(),
            patient: $("#patient").val(),
        };

        toggleResetButton(filters);

        $.ajax({
            url: "/Patient/FilterPatients",
            type: "GET",
            data: filters,
            success: function (data) {
                const tbody = $("table tbody");
                tbody.empty();

                if (data.length > 0) {
                    data.forEach(patient => {
                        tbody.append(`
                        <tr data-id="${patient.ID}">
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

    // Toggle reset button visibility based on filters
    function toggleResetButton(filters) {
        if (filters.date || filters.dosage || filters.drug || filters.patient) {
            $("#resetBtn").show();
        } else {
            $("#resetBtn").hide();
        }
    }

    $("#date, #dosage, #drug, #patient").on("keyup change", function () {
        ajaxFilterTable();
    });

    // Reset filters when reset button is clicked
    $("#resetBtn").on("click", function () {
        $("#date").val("");
        $("#dosage").val("");
        $("#drug").val("");
        $("#patient").val("");
        ajaxFilterTable();
    });

    // Initial table load and reset button visibility check
    ajaxFilterTable();
});
