// Xử lý gửi bình luận bằng AJAX
const form = document.getElementById('comment-form');
if (form) {
    form.addEventListener('submit', async function (e) {
        e.preventDefault(); // Chặn hành vi gửi form truyền thống

        const response = await fetch(form.action, {
            method: 'POST',
            body: new FormData(form)
        });
        const data = await response.json();

        const errorBox = document.getElementById('comment-error');
        if (!data.success) {
            errorBox.textContent = data.message;
            errorBox.classList.remove('d-none');
            return;
        }
        errorBox.classList.add('d-none');

        // Vẽ bình luận mới lên đầu danh sách
        const list = document.getElementById('comment-list');
        const noComment = document.getElementById('no-comment');
        if (noComment) noComment.remove();

        const item = document.createElement('div');
        item.className = 'border rounded p-3 mb-2';
        item.innerHTML = `
            <div class="d-flex justify-content-between">
                <strong></strong>
                <small class="text-muted"></small>
            </div>
            <p class="mb-1 mt-1"></p>`;
        item.querySelector('strong').textContent = data.fullName;
        item.querySelector('small').textContent = data.createdAt;
        item.querySelector('p').textContent = data.content;
        list.prepend(item);

        // Cập nhật số đếm + xóa trắng ô nhập
        const count = document.getElementById('comment-count');
        count.textContent = parseInt(count.textContent) + 1;
        form.querySelector('textarea').value = '';
    });
}

// Xử lý vote kiểu Reddit bằng AJAX
const btnUp = document.getElementById('btn-upvote');
const btnDown = document.getElementById('btn-downvote');

async function sendVote(isUpvote) {
    // Chưa đăng nhập → đưa về trang Login
    if (document.getElementById('vote-logged-in').value !== '1') {
        window.location.href = '/Account/Login';
        return;
    }

    const formData = new FormData(document.getElementById('vote-form'));
    formData.append('recipeId', document.getElementById('vote-recipe-id').value);
    formData.append('isUpvote', isUpvote);

    const response = await fetch('/Votes/Toggle', { method: 'POST', body: formData });
    const data = await response.json();
    if (!data.success) return;

    // Cập nhật điểm + màu nút theo trạng thái mới
    document.getElementById('vote-score').textContent = data.score;
    btnUp.className = 'btn btn-sm ' + (data.userVote === 'up' ? 'btn-warning' : 'btn-outline-secondary');
    btnDown.className = 'btn btn-sm ' + (data.userVote === 'down' ? 'btn-primary' : 'btn-outline-secondary');
}

if (btnUp && btnDown) {
    btnUp.addEventListener('click', () => sendVote(true));
    btnDown.addEventListener('click', () => sendVote(false));
}