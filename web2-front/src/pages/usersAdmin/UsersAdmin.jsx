import Table from "../../components/table/Table";
import { listAllAdmin } from "../../api/get";
import { useState } from "react";
import { useEffect } from "react";

const UsersAdmin = () => {
  const [rows, setRows] = useState([]);
  const columns = [
    "Username",
    "Email",
    "Name",
    "Lastname",
    "Birthday",
    "Address",
    "UserType",
    "UserStatus",
    "Picture",
  ];

  const getAll = async () => {
    const response = await listAllAdmin();
    if (response !== 200) {
    }
    const responseData = response.data;

    const mappedRows = responseData.map((item) => ({
      Username: item.username,
      Email: item.email,
      Name: item.name,
      Lastname: item.lastname,
      Birthday: item.birthday,
      Address: item.address,
      UserType: item.userType,
      UserStatus: item.userStatus,
      Picture: item.picture,
    }));
    setRows(mappedRows);
  };

  useEffect(() => {
    getAll();
    console.log(rows);
  }, []);

  return (
    <div>
      <Table columns={columns} rows={rows} />
    </div>
  );
};

export default UsersAdmin;
