function openModal() {
    document.getElementById('taskModal').style.display = 'block';
}

function closeModal() {
    document.getElementById('taskModal').style.display = 'none';
}

// Task management
let taskCounter = 1;

function addTask(event) {
    event.preventDefault();

    const title = document.getElementById('taskTitle').value;
    const desc = document.getElementById('taskDesc').value;
    const priority = document.getElementById('taskPriority').value;
    const category = document.getElementById('taskCategory').value;

    const taskCard = createTaskCard(title, desc, priority, category);
    document.getElementById('todo-column').appendChild(taskCard);

    closeModal();
    event.target.reset();
}

function createTaskCard(title, desc, priority, category) {
    const card = document.createElement('div');
    card.className = 'task-card';
    card.draggable = true;
    card.id = `task-${taskCounter++}`;

    card.innerHTML = `
        <div class="task-title">${title}</div>
        <div class="task-desc">${desc}</div>
        <div class="task-meta">
            <span>${category}</span>
            <span class="task-priority priority-${priority}">${priority}</span>
        </div>
    `;

    card.addEventListener('dragstart', drag);
    return card;
}

// Drag and drop functionality
function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    const data = ev.dataTransfer.getData("text");
    const draggedElement = document.getElementById(data);
    const dropZone = ev.target.closest('.task-column');

    if (dropZone && draggedElement) {
        dropZone.appendChild(draggedElement);
    }
}

// Category selection
document.querySelectorAll('.category-item').forEach(item => {
    item.addEventListener('click', () => {
        document.querySelector('.category-item.active').classList.remove('active');
        item.classList.add('active');
    });
});

// Initialize with sample tasks
window.onload = function () {
    const sampleTasks = [
        {
            title: "Design Review Meeting",
            desc: "Review latest UI/UX designs with the team",
            priority: "high",
            category: "work"
        },
        {
            title: "Grocery Shopping",
            desc: "Buy weekly groceries and household items",
            priority: "medium",
            category: "shopping"
        },
        {
            title: "Evening Workout",
            desc: "30 minutes cardio and strength training",
            priority: "low",
            category: "health"
        }
    ];

    sampleTasks.forEach(task => {
        const taskCard = createTaskCard(task.title, task.desc, task.priority, task.category);
        document.getElementById('todo-column').appendChild(taskCard);
    });
}