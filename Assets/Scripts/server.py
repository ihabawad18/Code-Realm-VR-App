from flask import Flask, request, jsonify
import subprocess
import os
app = Flask(__name__)

def execute_python_code(python_code):
    try:
        # Save the Python code to a temporary file
        with open('temp_code.py', 'w') as file:
            file.write(python_code)

        # Execute the Python file using a shell command
        command = f'python temp_code.py'  # or 'python3' depending on your Python version
        process = subprocess.Popen(command, shell=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        stdout, stderr = process.communicate()

        # Check if the execution was successful
        if process.returncode == 0:
            # Clean up the result by replacing '\r\n' with a newline
            cleaned_result = stdout.decode('utf-8').replace('\r\n', '\n').strip()
            print(cleaned_result)
            return {'success': True, 'result': cleaned_result}
        else:
            # Clean up the error message by replacing '\r\n' with a newline
            print("YES")
            cleaned_error = stderr.decode('utf-8').replace('\r\n', '\n').strip()
            return {'success': False, 'error': cleaned_error}
    except Exception as e:
        
        return {'success': False, 'error': str(e)}
    # Cleanup: Remove the temporary Python file
    # os.remove('temp_code.py')


@app.route('/execute_python', methods=['POST'])
def execute_python():
    try:
        # Receive the Python code from the POST request
        python_code = request.json['python_code']

        # Execute the Python code and capture the result
        result_dict = execute_python_code(python_code)

        return jsonify(result_dict)
    except Exception as e:
        return jsonify({'success': False, 'error': str(e)})

if __name__ == '__main__':
    app.run(debug=True)
