import React, { ReactElement, FC, useState, useEffect } from "react";
import { Box, Typography, TextField, Button } from "@mui/material";
import axios, { AxiosResponse, AxiosError } from "axios";

const ApiTest: FC<any> = (): ReactElement => {
  const [userId, setUserId] = useState<number | null>(null);
  const [userData, setUserData] = useState<any>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    
  }, []);

  const handleUserIdChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUserId(parseInt(event.target.value, 10));
  };

  const handleGetUser = () => {
    setError(null);
    axios
      .get(`https://reqres.in/api/users/${userId}`)
      .then((response: AxiosResponse) => {
        setUserData(response.data.data);
      })
      .catch((error: AxiosError) => {
        setError(`Error fetching user: ${error.message}`);
      });
  };

  const handleCreateUser = () => {
    setError(null);
    axios
      .post("https://reqres.in/api/users", { name: "John Doe", job: "Developer" })
      .then((response: AxiosResponse) => {
        setUserData(response.data);
      })
      .catch((error: AxiosError) => {
        setError(`Error creating user: ${error.message}`);
      });
  };

  return (
    <Box
      sx={{
        flexGrow: 1,
        backgroundColor: "whitesmoke",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Typography variant="h3">API Test</Typography>
      <Box sx={{ display: "flex", flexDirection: "column", alignItems: "center", mt: 2 }}>
        <TextField
          label="User ID"
          variant="outlined"
          type="number"
          value={userId || ""}
          onChange={handleUserIdChange}
          sx={{ mb: 1 }}
        />
        <Button variant="contained" color="primary" onClick={handleGetUser} sx={{ mb: 1 }}>
          Get User
        </Button>
        <Button variant="contained" color="primary" onClick={handleCreateUser}>
          Create User
        </Button>
        {userData && (
          <Box sx={{ mt: 2 }}>
            <Typography variant="subtitle1">User Data:</Typography>
            <pre>{JSON.stringify(userData, null, 2)}</pre>
          </Box>
        )}
        {error && (
          <Box sx={{ mt: 2, color: "red" }}>
            <Typography variant="subtitle1">{error}</Typography>
          </Box>
        )}
      </Box>
    </Box>
  );
};

export default ApiTest;
