document.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('#orderForm');
    const message = document.querySelector('#orderMessage');

    if (form) {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            const formData = new FormData(form);
            const response = await fetch(form.action, {
                method: 'POST',
                body: formData
            });
            const result = await response.json();
            message.textContent = result.message;
            form.reset();
        });
    }
});
