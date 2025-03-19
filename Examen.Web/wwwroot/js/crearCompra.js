const { createApp } = Vue;

const app = createApp({
    data() {
        return {
            productos: [
               
            ],
            compraCabecera: {

                Id_CompraCab: 0,
                FecRegistro: new Date().toISOString().split("T")[0],
                SubTotal: 0,
                Igv: 0,
                Total: 0,
                compraDetalle: []
            },
            nuevoDetalle: {
                Id_producto: "",
                Nombre_producto: "",
                Cantidad: 0,
                Costo:[],
                Precio: 0,
                Sub_Total: 0,
                Igv: 0,
                Total: 0
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
            costoEditable:0
        };
    },
    computed: {
        resultado() {
            return (this.costo * 1.35).toFixed(2); // Calcula automáticamente
        }
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
            const productoExiste = this.compraCabecera.compraDetalle.some(p => p.Id_producto === this.productoSeleccionado.id_Producto);
            if (productoExiste) {
                alert("Producto ya existe.");
                return;
            }


            this.nuevoDetalle.Id_producto = this.productoSeleccionado.id_Producto;
            this.nuevoDetalle.Nombre_producto = this.productoSeleccionado.nombre_producto;
            this.nuevoDetalle.Precio = this.precioEditable;
            this.nuevoDetalle.Sub_Total = this.nuevoDetalle.Cantidad * this.precioEditable;
            this.nuevoDetalle.Igv = this.nuevoDetalle.Sub_Total * 0.18;
            this.nuevoDetalle.Total = this.nuevoDetalle.Sub_Total + this.nuevoDetalle.Igv;


            // Agregamos el producto a la lista de detalles
            this.compraCabecera.compraDetalle.push({ ...this.nuevoDetalle });

          

            // Reseteamos el producto
            this.nuevoDetalle = { Id_producto: "", Cantidad: 0, Precio: 0, Sub_Total: 0, Igv: 0, Total: 0 };
            this.productoSeleccionado = null;

            // Recalculamos la cabecera
            this.calcularTotales();

            //limpiar campos
            this.limpiarCampos();

        },
        calcularTotales() {
            let subtotal = this.compraCabecera.compraDetalle.reduce((sum, item) => sum + (item.Cantidad * item.Precio), 0);

            this.compraCabecera.SubTotal = subtotal;
            this.compraCabecera.Igv = subtotal * 0.18;
            this.compraCabecera.Total = subtotal + this.compraCabecera.Igv;
        },
        guardarCompra() {
            debugger;
            const data = {
                compra: {
                    ...this.compraCabecera,
                    CompraDet: this.compraCabecera.compraDetalle
                }

            };
  
         
            fetch("/compra/CrearCompra", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(data => {
                    console.log("Compra guardada:", data);
                    /*   window.location.href = '/Compra/CompraIndex';*/
                    // Mostrar SweetAlert2
                    window.Swal.fire({
                        title: "¡Éxito!",
                        text: "Compra guardada correctamente",
                        icon: "success",
                        confirmButtonText: "OK"
                    }).then(() => {
                        // Redirigir al listado después de cerrar la alerta
                        window.location.href = "/Compra/CompraIndex";
                    });
                })
                .catch(error => console.error("Error:", error));
          
        },
        eliminarProducto(index) {
            this.compraCabecera.compraDetalle.splice(index, 1);
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
            } else {
                this.precioEditable = ""; // Limpiar si no hay producto seleccionado
            }
        }
    }
});

app.mount("#appCrearCompra");