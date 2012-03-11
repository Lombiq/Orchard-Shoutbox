(function ($) {
    $.extend({
        shoutbox: {
            loadMessages: function (fetchMessagesUrl, containerId) {
                $.get(fetchMessagesUrl, function (response) {
                    $("#" + containerId).html(response);
                });
            },

            initialize: function (formId, fetchMessagesUrl, containerId, hintMessage) {
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
                        that.loadMessages(fetchMessagesUrl, containerId);
                        return false;
                    }

                    var submitButton = form.find(".primaryAction");
                    submitButton.attr("disabled", "disabled");

                    textbox.attr("readonly", "readonly");

                    $.post(form.attr("action"), form.serialize(), function (response) {
                        that.loadMessages(fetchMessagesUrl, containerId);
                        textbox.val("");
                        textbox.removeAttr('readonly');
                        submitButton.removeAttr('disabled');
                    });

                    return false;
                });
            }
        }
    });
})(jQuery);