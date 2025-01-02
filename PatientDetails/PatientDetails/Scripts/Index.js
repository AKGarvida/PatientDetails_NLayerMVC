$(document).ready(function () {
    //$("#date").attr("placeholder", "dd-MM-yyyy");
    const rowsPerPage = 5; 
    const tableBody = $("table tbody");
    const pagination = $(".pagination");

    function renderTablePage(pageNumber) {
        const rows = tableBody.find("tr");
        const totalRows = rows.length;
        const totalPages = Math.ceil(totalRows / rowsPerPage);

        // Hide all rows
        rows.hide();

        // Show rows for the current page
        const start = (pageNumber - 1) * rowsPerPage;
        const end = start + rowsPerPage;
        rows.slice(start, end).show();

        // Update pagination
        updatePagination(pageNumber, totalPages);
    }

    function updatePagination(currentPage, totalPages) {
        pagination.empty();

        const previousDisabled = currentPage === 1 ? "disabled" : "";
        const nextDisabled = currentPage === totalPages ? "disabled" : "";

        pagination.append(`<li class="page-item ${previousDisabled}"><a class="page-link text-white bg-dark" href="#" data-page="${currentPage - 1}">Previous</a></li>`);

        // Calculate range for page numbers to display
        const startPage = Math.max(1, currentPage - 1);
        const endPage = Math.min(totalPages, currentPage + 1);

        for (let i = startPage; i <= endPage; i++) {
            const activeClass = currentPage === i ? "active" : "";
            pagination.append(`<li class="page-item ${activeClass}"><a class="page-link text-white bg-dark" href="#" data-page="${i}">${i}</a></li>`);
        }

        pagination.append(`<li class="page-item ${nextDisabled}"><a class="page-link text-white bg-dark" href="#" data-page="${currentPage + 1}">Next</a></li>`);
    }


    // Handle pagination click
    pagination.on("click", ".page-link", function (e) {
        e.preventDefault();
        const pageNumber = parseInt($(this).data("page"), 10);

        if (!isNaN(pageNumber)) {
            renderTablePage(pageNumber);
        }
    });

    // Initial rendering of the table
    renderTablePage(1);

    // ------------- Delete & Edit Modal Script -------------
    $(document).on('click', '.deleteBtn', function () {
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

                    // Show success modal
                    $('#successModal').modal('show');
                },
                error: function (xhr, status, error) {
                    console.error("Error deleting record:", error);
                    $('#actionModal').modal('hide');
                },
            });
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
                                <button type="button" class="btn editBtn btn-dark me-2" data-bs-toggle="tooltip" title="Edit Record"
                                onclick = "window.location.href = '/Patient/Edit/' + ${ patient.ID }">Edit</button>
                                <button type="button" class="btn deleteBtn btn-dark" data-bs-toggle="tooltip" title="Delete Record">Delete</button>
                            </td>
                            <td>${new Date(patient.ModifiedDate).toLocaleDateString()}</td>
                            <td>${parseFloat(patient.Dosage).toFixed(4)}</td>
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

                // Re-render table for current page after filtering
                renderTablePage(1);
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
