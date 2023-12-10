
import React, { useEffect, useState } from 'react';
import axios, { AxiosResponse, AxiosError } from 'axios';

interface Resource {
  id: number;
  name: string;
}

const ResourceList: React.FC = () => {
  const [resources, setResources] = useState<Resource[]>([]);

  useEffect(() => {
    axios.get('https://reqres.in/api/resources')
      .then((response: AxiosResponse) => setResources(response.data.data))
      .catch((error: AxiosError) => console.error('Error fetching resources:', error));
  }, []);

  return (
    <div>
      <h2>Resource List</h2>
      <ul>
        {resources.map(resource => (
          <li key={resource.id}>{resource.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default ResourceList;
