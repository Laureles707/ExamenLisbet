const { createApp } = Vue;

const app = Vue.createApp({
    data() {
        return {
            username: '',
            password: '',
            errorMessage: ''
        };
    },
    methods: {
        async login() {
            if (this.username === "" || this.password === "") {
                window.Swal.fire({
                    title: "Error",
                    text: "Debe ingresar usuario y contraseña",
                    icon: "warning",
                    confirmButtonText: "OK"
                });
                return; // Sale de la función sin continuar con el fetch
            }

            try {


                debugger;
                const obj = {
                    UserName: this.username,
                    Password: this.password


                };


                fetch("/Login/Login", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(obj)
                })
                    .then(response => response.json())
                    .then(data => {
                        debugger;
                        if (data.isSuccess) {
                            window.location.href = "/Home/Index";
                            console.log("Login exitoso:", data);

                            // Mostrar SweetAlert2
                            //window.Swal.fire({
                            //    title: "¡Éxito!",
                            //    text: "Bienvenido al sistema",
                            //    icon: "success",
                            //    confirmButtonText: "OK"
                            //}).then(() => {
                            //    // Redirigir al después de cerrar la alerta
                              
                            //});
                        }
                        else {
                            this.errorMessage = data.message;
                        }

                    })
                    .catch(error => {
                        // this.errorMessage = data.message || "Credenciales incorrectas";
                        console.error("Error:", error);

                    });

            } catch (error) {
                console.error("Error en la autenticación:", error);
                this.errorMessage = "Error en el servidor";
            }
        }
    }
});

app.mount("#loginApp");