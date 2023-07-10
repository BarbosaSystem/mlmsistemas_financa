import http from './config';

export default {
    Controller() {
        return "/ApplicationUser/";
    },
    async ChangePassword(formulario) {
        return await http.post('/ApplicationUser/ChangePassword', {
            "Id": formulario.Id,
            "OldPassword": formulario.OldPassword,
            "NewPassword": formulario.NewPassword,
            "ConfirmPassword": formulario.ConfirmPassword
        })
    },
    async Logar(formulario) {
        var request = await http.post(
            '/ApplicationUser/Login', {
                "UserName": formulario.UserName,
                "Password": formulario.Password
            }, {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        )
        alert("Seja Bem Vindo " + formulario.UserName)
        return request
    },
    async Logout() {
        localStorage.removeItem('Token')
        var token = localStorage.getItem('Token')
        return token;
    },
    async VerifyToken() {
        await http.post('/Token/VerifyToken/' + isToken())
    },
    async Registrar(formulario) {
        return await http.post(
            '/ApplicationUser/Register', formulario, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
    },
    async RecoverPassword(email) {
        return await http.post(this.Controller() + 'FindUserByEmail', {
            "Email": email
        })
    },
    async UserProfile() {
        return await http.get(
            '/UserProfile'
        ).catch(() => {})
    },
    async GetUsersList() {
        return await http.get(
            '/ApplicationUser/getUsuario', {
            }).catch(() => {})
    },
    async LoadImageProfile(formulario) {
        var form = new FormData();
        form.append('UserId', formulario.UserId)
        form.append('files', formulario.files)
        return await http.post(
            '/UserProfile/LoadImageProfile', form, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
    },

    async GetUserInfo(codigo) {
        return await http.get(
            '/UserProfile/GetUserById/' + codigo)
    },
    async ChangeEmailAndRole(formulario) {
        return await http.post('/UserProfile/ChangeEmailAndRole', formulario)
    }
}