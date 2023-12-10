import React, { useState } from 'react';
import axios, { AxiosResponse, AxiosError } from 'axios';

interface UserFormProps {
  onSubmit: () => void;
}

interface FormData {
  firstName: string;
  lastName: string;
}

const UserForm: React.FC<UserFormProps> = ({ onSubmit }) => {
  const [formData, setFormData] = useState<FormData>({
    firstName: '',
    lastName: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response: AxiosResponse = await axios.post('https://reqres.in/api/users', formData);
      console.log('User created successfully:', response.data);
      onSubmit();
    } catch (error: any) { 
      console.error('Error creating user:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Create User</h2>
      <label>
        First Name:
        <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} />
      </label>
      <label>
        Last Name:
        <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} />
      </label>
      <button type="submit">Create User</button>
    </form>
  );
};

export default UserForm;
