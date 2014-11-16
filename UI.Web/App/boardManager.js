(function boardManager() {
    "use strict";
    var currentState;
    var preloadedState;
    var address = "/api/Board";

    $(document).ready(function () {
        preloadState(function () {
            setInterval(function () {
                showNextGeneration();
            }, 850);
        });
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
        var html = template(preloadedState);
        $("#board-container").html(html);
        currentState = preloadedState;

        preloadState();
    }

    function preloadState(callback) {
        $.ajax({
            type: "POST",
            url: address,
            data: JSON.stringify(currentState),
            contentType: "application/json",
            success: function (response) {
                preloadedState = response;
            }
        });

        if (callback) callback();
    }
}(window.BoardManager = window.BoardManager || {}));