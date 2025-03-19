const { createApp } = Vue;

const app = createApp({
    data() {
        return {
            listaMovProductos: [

            ],
            productos: [

            ],

        };
    },
    computed: {
        
    },
    mounted() {
     
        this.listarProducto();
    },
    methods: {
        async listarProducto() {
           
            debugger;
            try {

                const response = await fetch(`/producto/listarProducto`, {
                    method: "GET"
                    
                });
                debugger;
                const data = await response.json();
                this.productos = data;
                if (!response.ok) {
                    throw new Error("Error al obtener los productos");
                }
               
            } catch (error) {
                console.error("Error:", error);
            }
        },
        async listarMovProducto(item) {
            console.log(item);

            debugger
            try {

                const response = await fetch(`/movimiento/getMovimientoProducto?id=${item}`, {
                    method: "GET"
                });
                const data = await response.json();
                this.listaMovProductos = data;
                if (!response.ok) {
                    throw new Error("Error al obtener los movimientos");
                }
     
            } catch (error) {
                console.error("Error:", error);
            }
        }
        
    },
    watch: {
     
    }
});

app.mount("#appMovimiento");