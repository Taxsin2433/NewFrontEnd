
import React, { useEffect, useState } from 'react';
import axios, { AxiosResponse, AxiosError } from 'axios';

interface User {
  id: number;
  first_name: string;
  last_name: string;
}

const UserList: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response: AxiosResponse = await axios.get('https://reqres.in/api/users');
        setUsers(response.data.data);
      } catch (error: unknown) {
        
        if (axios.isAxiosError(error)) {
          console.error('Axios Error fetching users:', error);
        } else {
          console.error('Unknown Error fetching users:', error);
        }
      }
    };

    
    fetchData();
  }, []); 

  return (
    <div>
      <h2>User List</h2>
      <ul>
        {users.map(user => (
          <li key={user.id}>{user.first_name} {user.last_name}</li>
        ))}
      </ul>
    </div>
  );
};

export default UserList;
