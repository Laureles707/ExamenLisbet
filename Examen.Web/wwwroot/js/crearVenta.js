const { createApp } = Vue;

const app = createApp({
    data() {
        return {
            productos: [
               
            ],
            ventaCabecera: {

                Id_VentaCab: 0,
                FecRegistro: new Date().toISOString().split("T")[0],
                SubTotal: 0,
                Igv: 0,
                Total: 0,
                ventaDetalle: []
            },
            nuevoDetalle: {
                Id_producto: "",
                Nombre_producto: "",
                Cantidad: 0,
                Costo:[],
                Precio: 0,
                Sub_Total: 0,
                Igv: 0,
                Total: 0,
                Stock:0
            },
            productoSeleccionado: "",
            cantidad: 0,
            precio: 0,
            totalGeneral: 0,
            subTotalGeneral: 0,
            totalImpuestoGeneral: 0,
            totalGeneral: 0,
            producto: [],
            nombreProducto: '',
            nroLote: 0,
            costo: 0,
            precioVenta: 0,
            stock: 0,
            precioEditable: 0,
            costoEditable: 0

        };
    },
    computed: {
        
    },
    mounted() {
        this.listarProducto(); // 🔹 Se ejecuta automáticamente al montar el componente
      
    },
    methods: {
        
       async listarProducto() {
            try {
                const response = await fetch("/producto/listarProducto"); // URL del API
                if (!response.ok) {
                    throw new Error("Error al obtener los productos");
                }
                this.productos = await response.json();
            } catch (error) {
                console.error("Error:", error);
            }
        },
        agregarDetalle() {
     
            if (!this.productoSeleccionado || this.nuevoDetalle.Cantidad <= 0 || this.producto.Precio <= 0) {
                alert("Ingrese todos los datos correctamente.");
                return;
            }
            debugger;
            //validar si producto ya existe en la lista
            const productoExiste = this.ventaCabecera.ventaDetalle.some(p => p.Id_producto === this.productoSeleccionado.id_Producto);
            if (productoExiste) {
                alert("Producto ya existe.");
                return;
            }
            if (this.nuevoDetalle.Cantidad > this.stock) {
                alert("Stock excede a la Cantidad.");
                return;
            }

            this.nuevoDetalle.Id_producto = this.productoSeleccionado.id_Producto;
            this.nuevoDetalle.Nombre_producto = this.productoSeleccionado.nombre_producto;
            this.nuevoDetalle.Precio = this.precioEditable;
            this.nuevoDetalle.Sub_Total = this.nuevoDetalle.Cantidad * this.precioEditable;
            this.nuevoDetalle.Igv = this.nuevoDetalle.Sub_Total * 0.18;
            this.nuevoDetalle.Total = this.nuevoDetalle.Sub_Total + this.nuevoDetalle.Igv;


            // Agregamos el producto a la lista de detalles
            this.ventaCabecera.ventaDetalle.push({ ...this.nuevoDetalle });

          

            // Reseteamos el producto
            this.nuevoDetalle = { Id_producto: "", Cantidad: 0, Precio: 0, Sub_Total: 0, Igv: 0, Total: 0 };
            this.productoSeleccionado = null;

            // Recalculamos la cabecera
            this.calcularTotales();

            //limpiar campos
            this.limpiarCampos();

        },
        calcularTotales() {
            let subtotal = this.ventaCabecera.ventaDetalle.reduce((sum, item) => sum + (item.Cantidad * item.Precio), 0);

            this.ventaCabecera.SubTotal = subtotal;
            this.ventaCabecera.Igv = subtotal * 0.18;
            this.ventaCabecera.Total = subtotal + this.ventaCabecera.Igv;
        },
        guardarVenta() {
            debugger;
            const data = {
                venta: {
                    ...this.ventaCabecera,
                    VentaDet: this.ventaCabecera.ventaDetalle
                }

            };
  
         
            fetch("/venta/CrearVenta", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(data => {
                    console.log("Venta guardada:", data);
                
                    window.Swal.fire({
                        title: "¡Éxito!",
                        text: "Venta guardada correctamente",
                        icon: "success",
                        confirmButtonText: "OK"
                    }).then(() => {
                        // Redirigir al listado después de cerrar la alerta
                        window.location.href = "/Venta/VentaIndex";
                    });
                })
                .catch(error => console.error("Error:", error));
          
        },
        eliminarProducto(index) {
            this.ventaCabecera.ventaDetalle.splice(index, 1);
            this.calcularTotales();
        },
        limpiarCampos() {
            this.nombreProducto = "";
            this.nroLote = 0;
            this.costo = 0;
            this.precioVenta = 0;
            this.stock = 0;
            this.precioEditable = 0;
            this.productoSeleccionado = "";
        },
        async guardarProducto() {
            if (this.nombreProducto == "" || this.nroLote <= 0 || this.costo <= 0 || this.precioVenta <= 0 || this.stock <= 0) {
                alert("Ingrese todos los datos correctamente.");
                return;
            }
            debugger;
            const producto = {
         
                Nombre_producto: this.nombreProducto,
                NroLote:this.nroLote,
                Costo: this.costo,
                PrecioVenta: this.precioVenta,
                Stock: this.stock
                

            };


           fetch("/producto/CrearProducto", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(producto)
            })
                .then(response => response.json())
                .then(data => {
                    console.log("Producto guardado:", data);
                    
                    window.Swal.fire({
                        title: "¡Éxito!",
                        text: "Producto guardado correctamente",
                        icon: "success",
                        confirmButtonText: "OK"
                    }).then(() => {
                        debugger;
                        this.limpiarCampos();
                        this.listarProducto();
                        
                        //const modalElement = document.getElementById("crearProductoModal");
                        //const modal = Modal.getInstance(modalElement) || new Modal(modalElement);

                        //modal.hide(); // Cierra el modal

                        if (this.modalInstance) this.modalInstance.hide();
                    });
                })
                .catch(error => console.error("Error:", error));

        },
       

    },
    watch: {
        productoSeleccionado(nuevoProducto) {
            debugger;
            if (nuevoProducto) {
                this.precioEditable = nuevoProducto.precioVenta; // Cargar el precio del producto seleccionado
                this.stock = nuevoProducto.stock; // Cargar el precio del producto seleccionado
            } else {
                this.precioEditable = ""; // Limpiar si no hay producto seleccionado
                this.stock = 0;
            }
        }
    }
});

app.mount("#appCrearVenta");