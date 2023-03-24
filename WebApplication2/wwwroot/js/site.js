<script>
    $(document).ready(function () {
    $('.book-row').click(function () {
        var bookId = $(this).find('td[data-id]').data('id');
        var url = '@Url.Action("BookDetail", "Home", new { id = "__id__" })'.replace('__id__', bookId);
        window.location.href = url;
    });
});
</script>
