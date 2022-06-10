function MostrarModal(titulo = "Deseas guardar este formulario?",
    texto = "Confirma por favor!") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0000FF',
        cancelButtonColor: '#FF0000',
        confirmButtonText: 'Si, guardar'       
    })
}

function MostrarModalDelete(titulo = "Deseas eliminar este registro?",
    texto = "Confirma por favor!") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0000FF',
        cancelButtonColor: '#FF0000',
        confirmButtonText: 'Si, eliminar'
    })
}

function MostrarModalEdit(titulo = "Deseas modificar este registro?",
    texto = "Confirma por favor!") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0000FF',
        cancelButtonColor: '#FF0000',
        confirmButtonText: 'Si, modificar'
    })
}

function MostrarSuccess() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'El formulario se guardó correctamente',
        showConfirmButton: false,
        timer: 6000,
        
    })
}

function MostrarSuccessEliminar() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Se eliminó correctamente',
        showConfirmButton: false,
        timer: 7000
    })
}

function MostrarSuccessEdit() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Se actualizó correctamente',
        showConfirmButton: false,
        timer: 7000
    })
}


function MostrarSuccessDoc() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'El documento se guardó correctamente',
        showConfirmButton: false,
        timer: 7000,

    })
}

