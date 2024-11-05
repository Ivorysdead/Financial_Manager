export async function apiPostRequest(path:any, body:any) {
    const apiBaseUrl = 'http://localhost:7208/api/SignUpFunction'; // Update this to your API base URL

    return fetch(`${apiBaseUrl}${path}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            // Add any required headers here, like authorization if needed
        },
        body: JSON.stringify(body),
    })
        .then(async (response) => {
            if (!response.ok) {
                throw new Error(`Error adding account: ${response.statusText}`);
            }
            return await response.json();
        })
        .catch((error) => {
            console.error('Error adding account:', error);
            return null; // Or handle it as needed
        });
}