import React, {useState} from 'react';
import './App.css';

// sample account data
const accounts = [
  { id: 1, type: 1, name: 'Emergency Fund', balance: 1200.0},
  { id: 2, type: 1, name: 'New Car Fund', balance: 625.0},
  { id: 4, type: 2, name: 'Fun Money', balance: 300.0},
  { id: 3, type: 3, name: '401K', balance: 10000.00}
];

function SwitchAPICall(){
  // TODO: SwitchAccount API Implementation
}

// Helper function: gets account type name
const getAccountTypeName = (type:number) => {
  switch (type) {
    case 1:
      return 'savings';
    case 2:
      return 'Checking';
    case 3:
      return 'Retirement';
    default:
      return 'Unknown Account';
  }
};

// Form Component: Active Account
function ActiveAccountForm({ account, onSwitch }: { account: any, onSwitch: () => void }) {
  return (
      <div>
        <h2>Current Active Account:</h2>
        <p>{`${getAccountTypeName(account.type)}: ${account.name}`}</p>
        <p>Balance: ${account.balance.toFixed(2)}</p>
        <button onClick={onSwitch}>Switch Account</button>
      </div>
  );
}

// Form Component: Switch Account
function SwitchAccountForm({ accounts, activeAccount, onSwitchAccount }: { accounts: any[], activeAccount: any, onSwitchAccount: (accountId: number) => void }) {
  const [selectedAccount, setSelectedAccount] = useState(activeAccount.id);

  return (
      <div>
        <h2>
            Accounts for user 10114:
        </h2>
        <table>
          <tbody>
          {accounts.map((account) => (
              <tr key={account.id}>
                <td>
                  <input
                      type="radio"
                      name="account"
                      checked={selectedAccount === account.id}
                      onChange={() => setSelectedAccount(account.id)}
                  />
                </td>
                <td>{getAccountTypeName(account.type)}</td>
                <td>{account.name}</td>
              </tr>
          ))}
          </tbody>
        </table>
        <button onClick={() => onSwitchAccount(selectedAccount)}>Switch Account</button>
      </div>
  );
}


// Form Component: Main
function App() {
    // Set "New Car Fund" as default account by finding it in the accounts array
    const defaultAccount = accounts.find(account => account.name === 'New Car Fund') || accounts[0];
    const [activeAccount, setActiveAccount] = useState(defaultAccount); // Default to "New Car Fund"
    const [isSwitching, setIsSwitching] = useState(false);

    const handleSwitchAccount = (accountId: number) => {
        const newAccount = accounts.find((acc) => acc.id === accountId);
        if (newAccount) {
            setActiveAccount(newAccount);
            setIsSwitching(false);
        }
    };

    return (
        <div className="App">
            <main className= "Main-content">
                {isSwitching ? (
                    <SwitchAccountForm
                        accounts={accounts}
                        activeAccount={activeAccount}
                        onSwitchAccount={handleSwitchAccount}
                    />
                ) : (
                    <ActiveAccountForm
                        account={activeAccount}
                        onSwitch={() => setIsSwitching(true)}
                    />
                )}
            </main>
        </div>
    );
}
export default App;
