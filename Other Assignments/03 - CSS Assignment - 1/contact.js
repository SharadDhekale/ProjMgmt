 
$(document).ready(function () {
    $('#user-form').validate({
        rules: {
            email: {
                required: true,
                email: true
            },
        },

        highlight: function (element, errorClass, validClass) {
            $(element).nextAll('.form-control-feedback').show().removeClass('glyphicon-ok').addClass('glyphicon-remove');
            $(element).addClass(errorClass).removeClass(validClass);
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).nextAll('.form-control-feedback').show().removeClass('glyphicon-remove').addClass('glyphicon-ok');
            element.closest('.form-group').removeClass('has-error').addClass('has-success');
            $(element).remove();
        }
    });
});
 

$('#contact-form').bootstrapValidator({
    //        live: 'disabled',
    message: 'This value is not valid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        Name: {
            validators: {
                notEmpty: {
                    message: 'The Name is required and cannot be empty'
                }
            }
        },
        email: {
            validators: {
                notEmpty: {
                    message: 'The email address is required'
                },
                emailAddress: {
                    message: 'The email address is not valid'
                }
            }
        },
        Message: {
            validators: {
                notEmpty: {
                    message: 'The Message is required and cannot be empty'
                }
            }
        }
    }
});