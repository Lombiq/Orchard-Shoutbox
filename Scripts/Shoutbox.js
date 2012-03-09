(function ($) {
    $.extend({
        shoutbox: {
            loadMessages: function (url, containerId) {
                $.get(url, function (response) {
                    $("#" + containerId).html(response);
                });
            },

            initialize: function (formId, url, containerId, hintMessage) {
                var form = $("#" + formId);

                var textbox = form.find("input[type=text]");
                textbox.val(hintMessage);
                textbox.focus(function () {
                    if (textbox.val() == hintMessage) textbox.val("");
                });
                textbox.blur(function () {
                    if (textbox.val() == "") textbox.val(hintMessage);
                });

                var that = this;

                form.submit(function () {
                    if (textbox.val() == "" || textbox.val() == hintMessage) {
                        that.loadMessages(url, containerId);
                        return false;
                    }

                    var submitButton = form.find(".primaryAction");
                    submitButton.attr("disabled", "disabled");
                    $.post(form.attr("action"), form.serialize(), function (response) {
                        that.loadMessages(url, containerId);
                        textbox.val("");
                        submitButton.removeAttr('disabled');
                    });

                    return false;
                });
            }
        }
    });
})(jQuery);