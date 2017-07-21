$(function () {
    $('.btn-Mark-Completed').on("click", function () {
        var id = $(this).closest('tr').attr('data-ord-id');
        console.log(id)
        $.post('/home/MarkCompleted/', { orderId: id }, function (result) {
            location.reload();
        });
    })

    $('.btn-Mark-Resolved').on("click", function () {
        var id = $(this).closest('tr').attr('data-issue-id');
        console.log(id)
        $.post('/home/MarkResolved/', { issueId: id }, function (result) {
            location.reload();
        });
    })
})