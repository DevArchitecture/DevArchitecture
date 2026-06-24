import { ref } from "vue";

const message = ref("");
const title = ref("");
const visible = ref(false);

export function useNotificationStore() {
  const showError = (msg: string, errorTitle = "Error") => {
    message.value = msg;
    title.value = errorTitle;
    visible.value = true;
    console.error(errorTitle, msg);
  };

  return {
    message,
    title,
    visible,
    showError,
  };
}
