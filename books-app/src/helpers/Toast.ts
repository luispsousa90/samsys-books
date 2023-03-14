import { toast } from 'react-toastify';

class ToastClass {
  Show(type: 'success' | 'error' | 'warning' | 'info', message: string) {
    toast[type](message, {
      position: 'top-center',
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: 'light',
    });
  }
}

var Toast = new ToastClass();
export default Toast;
