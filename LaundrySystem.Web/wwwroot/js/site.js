// site.js

// Genel bir başarı toast bildirimi göstermek için fonksiyon
function showSuccessToast(message) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2500,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
        }
    });

    Toast.fire({
        icon: 'success',
        title: message
    });
}

// Genel bir hata modal'ı göstermek için fonksiyon
function showErrorModal(title, message) {
    Swal.fire({
        icon: 'error',
        title: title,
        text: message,
        confirmButtonColor: '#00378d'
    });
}

// Genel bir bilgi modal'ı göstermek için fonksiyon
function showInfoModal(title, message) {
    Swal.fire({
        icon: 'info',
        title: title,
        text: message,
        confirmButtonColor: '#00378d'
    });
}