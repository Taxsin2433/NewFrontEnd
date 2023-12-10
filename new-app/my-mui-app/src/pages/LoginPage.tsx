import React, { ReactElement, useState } from "react";
import { Box, Typography, TextField, Button, Container, Paper } from "@mui/material";
import axios, { AxiosResponse, AxiosError } from "axios";

const LoginPage: React.FC = (): ReactElement => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);

  const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(e.target.value);
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const handleLogin = async () => {
    try {
      const response: AxiosResponse = await axios.post('https://reqres.in/api/login', { username, password });
      console.log('Login successful:', response.data);
      // Добавьте здесь логику для обработки успешного входа
    } catch (error: any) {
      if (axios.isAxiosError(error)) {
        console.error('Axios Error during login:', error);
        setError(`Error during login: ${error.message}`);
      } else {
        console.error('Unknown Error during login:', error);
        setError(`Unknown Error during login`);
      }
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <Paper elevation={3} sx={{ padding: 3, display: "flex", flexDirection: "column", alignItems: "center" }}>
        <Typography variant="h5">Login</Typography>
        <Box component="form" noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            label="Username"
            value={username}
            onChange={handleUsernameChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            label="Password"
            type="password"
            value={password}
            onChange={handlePasswordChange}
          />
          <Button
            type="button"
            fullWidth
            variant="contained"
            color="primary"
            onClick={handleLogin}
            sx={{ mt: 2 }}
          >
            Log In
          </Button>
          {error && (
            <Box sx={{ mt: 2, color: "red" }}>
              <Typography variant="subtitle1">{error}</Typography>
            </Box>
          )}
        </Box>
      </Paper>
    </Container>
  );
};

export default LoginPage;
