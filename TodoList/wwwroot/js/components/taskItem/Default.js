const tasks = documet.querySelectiorAll('.task');
tasks.forEach(task => {
    const id = task.dataset.id;
    let editBtn = task.querySelector('.edit');

    editBtn.addEventListener('click', () => {
        task.querySelector('[for="IsCompleted-${id}"]').classList.add('d-none');
        editbtn.classlist.add('disabled');
        task.querySelector('.editable-group').classlist.remove('d-none');
    });

    task.querySelector('.cancel').addEventListener('click', () => {
        location.reload();
    });
});