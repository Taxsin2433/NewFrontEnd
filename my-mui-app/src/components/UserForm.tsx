import React, { useState } from 'react';
import { observer } from 'mobx-react-lite';
import userStore from '../userStore';

interface UserFormProps {
  onSubmit: () => void;
}

const UserForm: React.FC<UserFormProps> = observer(({ onSubmit }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (name === 'email') {
      setEmail(value);
    } else if (name === 'password') {
      setPassword(value);
    }
  };

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await userStore.loginUser(email, password);
      console.log('Login successful');
      onSubmit();
    } catch (error: any) {
      console.error('Error logging in:', error.message);
    }
  };

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await userStore.registerUser(email, password);
      console.log('Registration successful');
      onSubmit();
    } catch (error: any) {
      console.error('Error registering user:', error.message);
    }
  };

  return (
    <div>
      <form onSubmit={handleLogin}>
        <h2>Login</h2>
        <label>
          Email:
          <input type="text" name="email" value={email} onChange={handleChange} />
        </label>
        <label>
          Password:
          <input type="password" name="password" value={password} onChange={handleChange} />
        </label>
        <button type="submit">Login</button>
      </form>

      <form onSubmit={handleRegister}>
        <h2>Register</h2>
        <label>
          Email:
          <input type="text" name="email" value={email} onChange={handleChange} />
        </label>
        <label>
          Password:
          <input type="password" name="password" value={password} onChange={handleChange} />
        </label>
        <button type="submit">Register</button>
      </form>
    </div>
  );
});

export default UserForm;
