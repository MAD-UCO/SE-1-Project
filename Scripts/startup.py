import subprocess
import os
import sys
import time

def validate(app_name:str, app_executable_path:str, app_path:str) -> bool:

    valid:bool = True

    if not os.path.exists(app_path):
        print(f"Path to {app_name} ({app_path!r}) does not exist or could not be found")
        valid = False

    elif not os.path.exists(app_executable_path):
        print(f"Path to {app_name}'s executable ({app_executable_path!r}) does not exist or could not be found")
        vlaid = False

    return valid

def run_processes(process_paths:list) -> None:
    print("Running the processes...")
    processes = []

    for proc_no, path in enumerate(process_paths):
        print(f"Running {path}")
        processes.append(
            subprocess.Popen([path], stdout=subprocess.PIPE, stderr=None)
        )


    print("All processes started")
    try:

        [proc.wait() for proc in processes]

    except (KeyboardInterrupt, Exception) as err:

        print(f"\n{type(err).__name__} occured, killing remaining processes...")
        print(err)

        for proc in processes:
            if proc.poll() is not None:
                proc.kill()
                

    print("All processes have terminated")


def main():

    num:str  =  input("Enter how many instances of the GUI you want (between 1 and 4 inclusive): ")
    
    input_valid:bool = num.isnumeric() and int(num) > 0 and int(num) < 5
    
    while not input_valid:
        print("Input invalid - ", end="")
        if not num.isnumeric():
            print("input must be an integer")
        elif int(num) <= 0:
            print("the number of instances must be at least 1")
        elif int(num) >= 5:
            print(f"{num} is too many instances")
        else:
            print("input a value")

        num:str = input("Enter how many instances of the GUI you want (between 1 and 4 inclusive): ")
        input_valid = num.isnumeric() and int(num) > 0 and int(num) < 5
    

    this_dir:str               =   os.path.abspath(os.path.dirname(__file__))

    gui_app_path:str            =   f"{this_dir}\\..\\SE-Semester-Project\\SE-Final-Project\\bin\\Debug\\"
    gui_app_exec:str            =   "SE-Final-Project.exe"
    gui_app:str                 =   f"{gui_app_path}{gui_app_exec}"
    
    server_app_path:str         =   f"{this_dir}\\..\\MoveBit-Server\\MoveBit-Server\\bin\\Debug\\"
    server_app_exec:str         =   "MoveBit-Server.exe"
    server_app:str              =   f"{server_app_path}{server_app_exec}"

    processes = []
    
    if validate('MoveBit GUI', gui_app_path, gui_app) and validate('MoveBit Server', server_app_path, server_app):
        for _ in range(int(num)):
            processes.append(gui_app)

        processes.append(server_app)

        run_processes(processes)

    else:
        print("Due to invalid paths, the process will not continue")
        print("Press <ENTER> to close this console")
        input()
        sys.exit(1)




if __name__ == "__main__":
    main()