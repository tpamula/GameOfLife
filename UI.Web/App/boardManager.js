(function boardManager() {
    "use strict";
    var previousResponse;

    $(document).ready(function () {
        showNextGeneration();

        setInterval(function () {
            showNextGeneration();
        }, 400);
    });

    Handlebars.registerHelper('if_eq', function (a, b, opts) {
        if (a === b) return opts.fn(this);
        else return opts.inverse(this);
    });

    var rawTemplate = "<table id='board'>{{#each Presentation}}" +
        "<tr>{{#each this}}" +
        "<td {{#if_eq this 1}}class='filled'{{/if_eq}}></td>" +
        "{{/each}}</tr>" +
        "{{/each}}</table>";

    var template = Handlebars.compile(rawTemplate);

    function showNextGeneration() {
        console.log("next generation");
        var address = "/api/Board";

        $.ajax({
            type: "POST",
            url: address,
            data: JSON.stringify(previousResponse),
            contentType: "application/json",
            success: function (response) {
                previousResponse = response;
                var html = template(response);
                $("#board-container").html(html);
            }
        });
    }
}(window.BoardManager = window.BoardManager || {}));