// Vote từ trang danh sách — nhiều nút, nhận diện qua data-recipe-id
document.querySelectorAll('.vote-btn').forEach(btn => {
    btn.addEventListener('click', async function () {
        if (document.getElementById('vote-logged-in').value !== '1') {
            window.location.href = '/Account/Login';
            return;
        }

        const recipeId = this.dataset.recipeId;
        const isUpvote = this.dataset.direction === 'up';

        const formData = new FormData(document.getElementById('vote-form'));
        formData.append('recipeId', recipeId);
        formData.append('isUpvote', isUpvote);

        const response = await fetch('/Votes/Toggle', { method: 'POST', body: formData });
        const data = await response.json();
        if (!data.success) return;

        // Cập nhật điểm + màu 2 nút của đúng card này
        document.querySelector(`.vote-score[data-recipe-id="${recipeId}"]`).textContent = data.score;
        const up = document.querySelector(`.vote-btn[data-recipe-id="${recipeId}"][data-direction="up"]`);
        const down = document.querySelector(`.vote-btn[data-recipe-id="${recipeId}"][data-direction="down"]`);
        up.className = 'btn btn-sm py-0 px-1 vote-btn ' + (data.userVote === 'up' ? 'btn-warning' : 'btn-outline-secondary');
        down.className = 'btn btn-sm py-0 px-1 vote-btn ' + (data.userVote === 'down' ? 'btn-primary' : 'btn-outline-secondary');
    });
});