﻿var areas = [
    { text: "Avaskär", value: "1" },
    { text: "Elleholm", value: "1" },
    { text: "Karlshamn", value: "1" },
    { text: "Karlskrona", value: "1" },
    { text: "Kristianopel", value: "1" },
    { text: "Lyckå", value: "1" },
    { text: "Ronneby", value: "1" },
    { text: "Sölvesborg", value: "1" },
    { text: "Konghälla", value: "2" },
    { text: "Kungälv", value: "2" },
    { text: "Lysekil", value: "2" },
    { text: "Marstrand", value: "2" },
    { text: "Strömstad", value: "2" },
    { text: "Uddevalla", value: "2" },
    { text: "Avestad", value: "3" },
    { text: "Borlänge", value: "3" },
    { text: "Falun", value: "3" },
    { text: "Hedemora", value: "3" },
    { text: "Ludvika", value: "3" },
    { text: "Säter", value: "3" },
    { text: "Åmål", value: "4" },
    { text: "Visby", value: "5" },
    { text: "Gävle", value: "6" },
    { text: "Sandviken", value: "6" },
    { text: "Falkenberg", value: "7" },
    { text: "Varberg", value: "7" },
    { text: "Halmstad", value: "7" },
    { text: "Kungsbacka", value: "7" },
    { text: "Laholm", value: "7" },
    { text: "Bollnäs", value: "8" },
    { text: "Hudiksvall", value: "8" },
    { text: "Söderhamn", value: "8" },
    { text: "Sveg", value: "9" },
    { text: "Östersund", value: "10" },
    { text: "Kiruna", value: "11" },
    { text: "Lycksele", value: "11" },
    { text: "Sundsvall", value: "12" },
    { text: "Boden", value: "13" },
    { text: "Haparanda", value: "13" },
    { text: "Luleå", value: "13" },
    { text: "Piteå", value: "13" },
    { text: "Askersund", value: "14" },
    { text: "Kumla", value: "14" },
    { text: "Örebro", value: "14" },
    { text: "Båstad", value: "15" },
    { text: "Eslöv", value: "15" },
    { text: "Falsterbo", value: "15" },
    { text: "Helsingborg", value: "15" },
    { text: "Hässleholm", value: "15" },
    { text: "Höganäs", value: "15" },
    { text: "Kristianstad", value: "15" },
    { text: "Landskrona", value: "15" },
    { text: "Lomma", value: "15" },
    { text: "Lund", value: "15" },
    { text: "Malmö", value: "15" },
    { text: "Simrishamn", value: "15" },
    { text: "Skanör", value: "15" },
    { text: "Trelleborg", value: "15" },
    { text: "Ystad", value: "15" },
    { text: "Åhus", value: "15" },
    { text: "Ängelholm", value: "15" },
    { text: "Eksjö", value: "16" },
    { text: "Gränna", value: "16" },
    { text: "Huskvarna", value: "16" },
    { text: "Jönköping", value: "16" },
    { text: "Kalmar", value: "16" },
    { text: "Ljungby", value: "16" },
    { text: "Nybro", value: "16" },
    { text: "Nässjö", value: "16" },
    { text: "Oskarshamn", value: "16" },
    { text: "Tranås", value: "16" },
    { text: "Vetlanda", value: "16" },
    { text: "Vimmerby", value: "16" },
    { text: "Värnamo", value: "16" },
    { text: "Västervik", value: "16" },
    { text: "Växjö", value: "16" },
    { text: "Eskilstuna", value: "17" },
    { text: "Flen", value: "17" },
    { text: "Katrineholm", value: "17" },
    { text: "Mariefred", value: "17" },
    { text: "Nacka", value: "17" },
    { text: "Nyköping", value: "17" },
    { text: "Nynäshman", value: "17" },
    { text: "Oxelösund", value: "17" },
    { text: "Strängnäs", value: "17" },
    { text: "Södertälje", value: "17" },
    { text: "Trosa", value: "17" },
    { text: "Stockholm", value: "17" },
    { text: "Stockholm", value: "18" },
    { text: "Djursholm", value: "18" },
    { text: "Enköping", value: "18" },
    { text: "Lidingö", value: "18" },
    { text: "Norrtälje", value: "18" },
    { text: "Sigtuna", value: "18" },
    { text: "Solna", value: "18" },
    { text: "Uppsala", value: "18" },
    { text: "Vaxholm", value: "18" },
    { text: "Arvika", value: "19" },
    { text: "Filipstad", value: "19" },
    { text: "Hagfors", value: "19" },
    { text: "Karlskoga", value: "19" },
    { text: "Karlstad", value: "19" },
    { text: "Kristinehamn", value: "19" },
    { text: "Säffle", value: "19" },
    { text: "Skelefteå", value: "20" },
    { text: "Umeå", value: "20" },
    { text: "Allingsås", value: "21" },
    { text: "Borås", value: "21" },
    { text: "Falköping", value: "21" },
    { text: "Hjo", value: "21" },
    { text: "Lidköping", value: "21" },
    { text: "Mariestad", value: "21" },
    { text: "Möndal", value: "21" },
    { text: "Skara", value: "21" },
    { text: "Skövde", value: "21" },
    { text: "Tidaholm", value: "21" },
    { text: "Trollhättan", value: "21" },
    { text: "Ulricehamn", value: "21" },
    { text: "Vänersborg", value: "21" },
    { text: "Älvsborg", value: "21" },
    { text: "Göteborg", value: "21" },
    { text: "Göteborg", value: "2" },
    { text: "Arboga", value: "22" },
    { text: "Fagerstad", value: "22" },
    { text: "Köping", value: "22" },
    { text: "Lindesberg", value: "22" },
    { text: "Nora", value: "22" },
    { text: "Sala", value: "22" },
    { text: "Västerås", value: "22" },
    { text: "Härnösand", value: "23" },
    { text: "Kramfors", value: "23" },
    { text: "Sollefteå", value: "23" },
    { text: "Örnsköldsvik", value: "23" },
    { text: "Borgholm", value: "24" },
    { text: "Hästholmen", value: "25" },
    { text: "Linköping", value: "25" },
    { text: "Mjölby", value: "25" },
    { text: "Motala", value: "25" },
    { text: "Norrköping", value: "25" },
    { text: "Skänninge", value: "25" },
    { text: "Söderköping", value: "25" },
    { text: "Vadstena", value: "25" },
];


$(document).ready(function () {
    //selectize
    $('.multi-select').selectize({});
    //fancybox
    $("a.grouped_elements").fancybox();

    //When selected county, the cities in that county displays
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

    //Fetch unread message count
    $.get("/Messages/GetUnreadCount", function (data) {
        if (data.count > 0) {
            $("#message-badge").text(data.count);
            //change color if count < 1
            $("#message-badge").addClass("label label-danger");
            //Twinkle twinkle
            setInterval(function () {
                $("#message-badge").fadeTo(1400, 0.1);
                $("#message-badge").fadeTo(700, 1.0);
            }, 4000)
        }
        else {
            $("#message-badge").text(0);
            //change color if count > 1
            $("#message-badge").addClass("label label-default");
        }
    });
});
