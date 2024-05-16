const express = require('express'); // Import Express framework
const { createClient } = require('@supabase/supabase-js'); // Import Supabase client

// Supabase configuration with URL and Anon Key
const supabaseUrl = 'https://tpwvqeodqsswzaixarnm.supabase.co';
const supabaseAnonKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InRwd3ZxZW9kcXNzd3phaXhhcm5tIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTUwMTY3NzksImV4cCI6MjAzMDU5Mjc3OX0.jbf_zWIAovuujlaAshVqLTfyq-Al-PX_wOqZRgD7qPs';
const supabase = createClient(supabaseUrl, supabaseAnonKey); // Initialize Supabase client

const app = express(); // Create an Express application
const port = 3000; // Define the port number for the server

app.use(express.json()); // Middleware to parse JSON request bodies

// Define a GET endpoint to retrieve user data
app.get('/user-data', async (req, res) => {
  const token = req.headers.token; // Get token from request headers
  const { data, error } = await supabase
    .from('Users')
    .select('*') // Select all columns
    .eq('token', token); // Filter by token

  if (error) return res.status(401).json({ error: 'Unauthorized' }); // Return 401 if error
  res.json(data); // Return user data as JSON
});

// Start the server and listen on the defined port
app.listen(port, () => {
  console.log(`Server started on http://localhost:${port}`);
});
