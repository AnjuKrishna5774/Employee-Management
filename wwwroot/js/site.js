jQuery(document).ready(function () {
    function isPassword(password) {
        
        var re = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        return re.test(password);
        //var regex = / ^ (?=.* [a - z])(?=.* [A - Z])(?=.*\d)(?=.* [^\da - zA - Z]).{ 8, 15 } $/;
        //return regex.test(email);
    }

    jQuery('#empl_password').on("change", function () {
       
        var m = isPassword(jQuery(this).val())
        if (m == false) {
            jQuery(this).val('')
            jQuery(this).focus()
            return false;
        }
        else {
            return true;
        }
    });


    jQuery('#empl_email').change(function () {

        var student_id = jQuery('#empl_id').val();
        if (typeof student_id == 'undefined') {
            student_id = 0
        }
        
        var empl_email = jQuery(this).val();
       
        jQuery.ajax({
            type: "GET",
            url: "/Employee/CheckEmployeeEmailUnqnes",
            data: { "empl_id": student_id, "empl_email": empl_email },
            contentType: "application/json,charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (parseInt(response) > 0) {

                    $('#empl_email').val("");
                    $('#empl_email').focus();
                    alert("This Email already exist");
                }
            }
        });

    });

});