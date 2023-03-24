// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>$(document).ready(function () {
    $('#search-form').on('submit', function (e) {
        e.preventDefault(); // отменяем стандартное действие формы

        // Получаем значение из поля ввода
        var searchValue = $('#search-input').val();

        // Отправляем запрос на сервер
        $.ajax({
            url: '@Url.Action("SearchActionResult", "Home")', // указываем адрес действия Search в контроллере Home
            type: 'POST',
            data: { searchValue: searchValue }, // передаем значение поискового запроса в виде параметра
            success: function (result) {
                // Обработка результата поиска
                console.log(result);
            }
        });
    })};
});</script>