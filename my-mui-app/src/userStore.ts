// userStore.ts

import { makeAutoObservable } from 'mobx';
import axios from 'axios';

interface UserData {
  id: number;
  // Добавьте другие поля, если они присутствуют в вашем объекте
}

class UserStore {
  users: UserData[] = [];
  currentUser = null;

  constructor() {
    makeAutoObservable(this);
  }

  async createUser(firstName: string, lastName: string) {
    try {
      const response = await axios.post('https://reqres.in/api/users', { firstName, lastName });
      this.users.push(response.data as UserData); // Приводим к типу UserData
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  async loginUser(email: string, password: string) {
    try {
      const response = await axios.post('https://reqres.in/api/login', { email, password });
      this.currentUser = response.data;
      return response.data;
    } catch (error) {
      throw error;
    }
  }

  async registerUser(email: string, password: string) {
    try {
      const response = await axios.post('https://reqres.in/api/register', { email, password });
      this.currentUser = response.data;
      return response.data;
    } catch (error) {
      throw error;
    }
  }
  async logout() {
    try {
      // Ваш запрос на выход пользователя с использованием axios или другой библиотеки
      // Пример:
      // const response = await axios.post('https://reqres.in/api/logout');
      
      // Сбрасываем информацию о текущем пользователе
      this.currentUser = null;

      // Возвращаем ответ (если нужно)
      // return response.data;
    } catch (error) {
      throw error;
    }
}}

const userStore = new UserStore();
export default userStore;
