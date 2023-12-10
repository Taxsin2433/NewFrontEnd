import React, { ReactElement, useState } from "react";
import { Box, Typography, TextField, Button, Container, Paper } from "@mui/material";
import axios, { AxiosResponse, AxiosError } from "axios";

const RegistrationPage: React.FC = (): ReactElement => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const handleRegistration = async () => {
    try {
      const response: AxiosResponse = await axios.post('https://reqres.in/api/register', { email, password });
      console.log('Registration successful:', response.data);
      // Добавьте здесь логику для обработки успешной регистрации
    } catch (error: any) {
      if (axios.isAxiosError(error)) {
        console.error('Axios Error during registration:', error);
        setError(`Error during registration: ${error.message}`);
      } else {
        console.error('Unknown Error during registration:', error);
        setError(`Unknown Error during registration`);
      }
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <Paper elevation={3} sx={{ padding: 3, display: "flex", flexDirection: "column", alignItems: "center" }}>
        <Typography variant="h5">Registration</Typography>
        <Box component="form" noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            label="Email"
            type="email"
            value={email}
            onChange={handleEmailChange}
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
            onClick={handleRegistration}
            sx={{ mt: 2 }}
          >
            Register
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

export default RegistrationPage;
