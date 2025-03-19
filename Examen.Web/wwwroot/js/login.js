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
                        console.log("Login exitoso:", data);
                        window.location.href = "/Home/Index";
                        // Mostrar SweetAlert2
                        window.Swal.fire({
                            title: "¡Éxito!",
                            text: "Bienvenido al sistema",
                            icon: "success",
                            confirmButtonText: "OK"
                        }).then(() => {
                            // Redirigir al después de cerrar la alerta
                           
                        });
                    })
                    .catch(error => {
                       // this.errorMessage = data.message || "Credenciales incorrectas";
                        console.error("Error:", error)
                    });

            } catch (error) {
                console.error("Error en la autenticación:", error);
                this.errorMessage = "Error en el servidor";
            }
        }
    }
});

app.mount("#loginApp");