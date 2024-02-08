import React, { useState, useEffect } from 'react';

export interface ITask {
    id: number;
    title: string;
    completed: boolean;
    iseditmode: boolean;
}

const TaskManager = () => {
  const [tasks, setTasks] = useState<ITask[]>([]);
  const [taskTitle, setTaskTitle] = useState('');

  useEffect(() => {
    const storedTasks = localStorage.getItem('tasks');
    if (storedTasks) {
      setTasks(JSON.parse(storedTasks));
    }
  }, []);

  useEffect(() => {
    if(tasks.length === 0) {
        return;
    }
    localStorage.setItem('tasks', JSON.stringify(tasks));
  }, [tasks]); // Save tasks to local storage whenever tasks state changes

  const handleInputChange = (event) => {
    setTaskTitle(event.target.value);
  };

  const handleAddtask = () => {
    if (taskTitle.trim() !== '') {
        setTasks([...tasks, { title: taskTitle, id: Date.now() , completed: false, iseditmode:false }]);
      setTaskTitle('');
    }
  };

  const handleDeleteTask = (id:number) => {
    const updatedTasks = tasks.filter(task => task.id !== id);
    setTasks(updatedTasks);
  };

  const handleEditTask = (id:number) => {
    const editTasks = tasks.map(task => {
        if(task.id === id) {
            return {
                ...task,
                iseditmode: true
            };
        }
        return task;
    });
    setTasks(editTasks);
  };

  const handleUpdateTask = (id:number, taskTitleValue: string) => {

    const updatedTasks = tasks.map(task => {
        if(task.id === id) {
            return {
                ...task,
                title: taskTitleValue,
                iseditmode: false
            };
        }
        return task;
    });
    setTasks(updatedTasks);
  };

  const handleCompleteEvent = (index:number) => {
    setTasks(tasks.map(task => {
        if(task.id === index) {
            return {
                ...task,
                completed: !task.completed
            };
        }
        return task;
    }));
};

  return (
    <div className='TaskWrapper'>
      <h1>Task App</h1>
      <div className='TaskForm'>
      <input
        type="text" className='task-input'
        value={taskTitle}
        onChange={handleInputChange}
        placeholder="Enter a new task Name"
      />
      <button className='task-btn' onClick={handleAddtask}>Add task</button>
      </div>
      <div className='TaskList'>
      <ul>
        {tasks.map(task => (
            task.iseditmode ? (<>
            <input
                type="text" className='task-input'
                title={task.title}
              />
              <button className='task-btn' 
              onClick={() => handleUpdateTask(task.id, task.title)}>Update</button>
              </>):
            (<li key={task.id} className='Task'>
            {task.title}
            <input type="checkbox" checked={task.completed} onChange={()=>handleCompleteEvent(task.id)}/>
            <button className='task-btn' onClick={() => handleEditTask(task.id)}>Edit</button>
            <button className='task-btn' onClick={() => handleDeleteTask(task.id)}>Delete</button>
          </li>)
        ))}
      </ul>
      </div>
    </div>
  );
};

export default TaskManager;
