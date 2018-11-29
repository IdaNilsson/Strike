// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var areas = [
    { text: "Malmö", value: "10" },
    { text: "Helsingborg", value: "10" },
    { text: "Trelleborg", value: "10" },
    { text: "Mjölby", value: "20" },
];

$(document).ready(function () {
    $('#county-select').on('change', function () {
        var area = $(this).find(':selected').attr('data-area');
        var areaSelect = $('#area-select');
        areaSelect.children().remove();
        var areasToAppend = areas.filter(function (item) {
            if (item.value === area) {
                return item;
            }
        });
        console.log("append", areasToAppend);
        for (var i = 0; i < areasToAppend.length; i++) {
            areaSelect.append('<option value="' + areasToAppend[i].text + '">' + areasToAppend[i].text + '</option>');
        }
    });
});
